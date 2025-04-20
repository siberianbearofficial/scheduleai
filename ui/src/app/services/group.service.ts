export class Group {
  constructor(
    public readonly group: string,
    public readonly disabled = false,
  ) {}

  public toString(): string {
    return `${this.group}`;
  }
}
