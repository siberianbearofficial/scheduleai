from pydantic import BaseModel, Field
from openai_api.schema import *


class Pair(BaseModel):
    teachers: Optional[list[str]] = None
    groups: Optional[list[str]] = None
    startTime: datetime
    endTime: datetime
    rooms: Optional[list[str]] = None
    discipline: Optional[str] = None
    actType: Optional[str] = None


class MergedPair(BaseModel):
    startTime: datetime
    endTime: datetime
    actType: Optional[str] = None
    discipline: Optional[str] = None
    rooms: Optional[list[str]] = None
    convenience: float = Field(..., description="Удобство - насколько удобно (от 0 до 1) исходя из всех факторов подойти к преподаветелю в это время")
    collisions: Optional[list[Pair]] = None
    waitTime: Optional[str] = None  # Assuming date-span format is string


class ResponseFormat(BaseModel):
    function_calling_needed: bool = Field(..., description="Требуется вызов одной или нескольких предоствленных функций или нет.")
    clarification_needed: bool  = Field(..., description="Нужны уточнения у пользователя. Например, запрос некорректный, то есть не хватает информации или же информация неверна." \
    "Не может быть True одновременно с function_calling_needed, то есть если нужны уточнения у пользователя вызов функций запрещен.")
    message: str = Field(..., description="Ответ в произвольном текстовом формате.")
    schedule: Optional[list[MergedPair]] = Field(None, description="Задавать, только если конечное расписание (ответ для пользователя) было получено из get_merged_schedule. Ответ из get_merged_schedule полностью соответствует этой json-схеме")



