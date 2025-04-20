from datetime import datetime

from bff_interaction.schema import ResponseFormat
from openai_api.schema import *


def get_current_date() -> str:
    return datetime.now().isoformat()


RESP_SCHEMA = str(ResponseFormat.model_json_schema())

# контексты работы агента
AGENT_ROLE_CONTEXT = MessageModel(role=OpenAIRole.SYSTEM,
                                   content="Ты умный ассисент-расписание. У тебя есть функции, которые можно вызывать, чтобы узнавать расписание студентов и преподавателей.")

RESP_FORMAT_CONTEXT = MessageModel(role=OpenAIRole.SYSTEM,
                                   content=f"Всегда в ответ возвращай сообщение в json формате по следующей схеме:\n{RESP_SCHEMA}")

RESP_DETAILS_CONTEXT = MessageModel(role=OpenAIRole.SYSTEM,
                                    content="1) Если в процессе требуется расписание преподавателя, для которого найдено" \
                                    "несколько совпадений (например несколько преподавателей с одной фамилией), то нужно уточнить у студента, какой именно преподаватель ему нужен\n" \
                                    "2) Если студент спрашивает, когда ему удобно посетить того или иного преподавателя, нужно использовать функцию get_merged_schedule, предварительно получив" \
                                    "teacher_id через get_teachers_by_name")

INIT_CONTEXT = MessagesModel(
    root=[AGENT_ROLE_CONTEXT, RESP_FORMAT_CONTEXT, RESP_DETAILS_CONTEXT]
)


