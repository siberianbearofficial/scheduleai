using System.Net.Http.Json;
using System.Text.Json;
using Bmstu.Mock.Models;

namespace Bmstu.Mock;

internal class Parser
{
    public List<BmstuGroup> Groups { get; set; } = [];
    public List<BmstuTeacher> Teachers { get; set; } = [];

    private readonly HttpClient _httpClient = new();

    public async Task ParseAsync()
    {
        foreach (var groupNode in FindNodes([await GetStructureAsync()], n => n.Type == "group")
                     .Where(g => g.Abbr?.StartsWith("ИУ7") == true))
        {
            Groups.Add(new BmstuGroup
            {
                Id = groupNode.Id.ToString() ?? throw new NullReferenceException(),
                Name = groupNode.Abbr ?? throw new NullReferenceException(),
            });
        }

        foreach (var group in Groups)
        {
            try
            {
                var schedule = await GetScheduleAsync(Guid.Parse(group.Id));
                foreach (var pair in schedule.Data)
                {
                    try
                    {
                        var bmstuPair = PairToBmstuPair(pair);
                        if (bmstuPair != null)
                        {
                            group.Pairs.Add(bmstuPair);
                        }

                        foreach (var teacher in pair.Teachers)
                        {
                            Console.WriteLine($"Found teacher {teacher.FirstName} {teacher.LastName}");
                            var bmstuTeacher = Teachers.FirstOrDefault(t => t.Id == teacher.Id.ToString());
                            if (bmstuTeacher == null)
                            {
                                bmstuTeacher = new BmstuTeacher
                                {
                                    Id = teacher.Id.ToString() ?? throw new NullReferenceException(),
                                    FullName = $"{teacher.LastName} {teacher.FirstName} {teacher.MiddleName}",
                                };
                                Console.WriteLine($"Adding teacher {bmstuTeacher.FullName}");
                                Teachers.Add(bmstuTeacher);
                            }

                            if (bmstuPair != null &&
                                bmstuTeacher.Pairs.FirstOrDefault(p => p.StartTime == bmstuPair.StartTime) == null)
                            {
                                bmstuTeacher.Pairs.Add(bmstuPair);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private static BmstuPair? PairToBmstuPair(Pair pair)
    {
        if (pair.StartTime == null || pair.EndTime == null)
            return null;
        var pairDay = pair.Day switch
        {
            1 => DayOfWeek.Monday,
            2 => DayOfWeek.Tuesday,
            3 => DayOfWeek.Wednesday,
            4 => DayOfWeek.Thursday,
            5 => DayOfWeek.Friday,
            6 => DayOfWeek.Saturday,
            7 => DayOfWeek.Sunday,
            _ => DayOfWeek.Monday,
        };
        var daysDelta = pairDay - DateTime.Today.DayOfWeek;
        if (daysDelta < 1)
            daysDelta += 7;
        return new BmstuPair
        {
            ActType = pair.Discipline?.ActType,
            Discipline = string.IsNullOrEmpty(pair.Discipline?.ShortName)
                ? string.IsNullOrEmpty(pair.Discipline?.FullName)
                    ? pair.Discipline?.Abbr
                    : pair.Discipline.FullName
                : pair.Discipline.ShortName,
            Rooms = pair.Audiences.Select(e => e.Name ?? "").Where(e => !string.IsNullOrEmpty(e)).ToArray(),
            StartTime = new DateTime(DateOnly.FromDateTime(DateTime.Today).AddDays(daysDelta),
                TimeOnly.Parse(pair.StartTime)),
            EndTime = new DateTime(DateOnly.FromDateTime(DateTime.Today).AddDays(daysDelta),
                TimeOnly.Parse(pair.EndTime)),
            Groups = pair.Groups.Select(g => g.Id.ToString() ?? "").ToArray(),
            Teachers = pair.Teachers.Select(t => t.Id.ToString()).ToArray(),
        };
    }

    private static IEnumerable<Node> FindNodes(IEnumerable<Node> nodes, Predicate<Node> predicate)
    {
        foreach (var node in nodes)
        {
            if (predicate(node))
            {
                yield return node;
            }

            foreach (var res in FindNodes(node.Children, predicate))
            {
                yield return res;
            }
        }
    }

    private async Task<Node> GetStructureAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<DataModel<Node>>(
            "https://lks.bmstu.ru/lks-back/api/v1/structure");
        return data?.Data ?? throw new NullReferenceException();
    }

    private async Task<Schedule> GetScheduleAsync(Guid groupId)
    {
        Console.WriteLine($"https://lks.bmstu.ru/lks-back/api/v1/schedules/groups/{groupId}/public");
        Console.WriteLine(await _httpClient.GetStringAsync(
            $"https://lks.bmstu.ru/lks-back/api/v1/schedules/groups/{groupId}/public"));
        var data = await _httpClient.GetFromJsonAsync<DataModel<Schedule>>(
            $"https://lks.bmstu.ru/lks-back/api/v1/schedules/groups/{groupId}/public");
        return data?.Data ?? throw new NullReferenceException();
    }
}