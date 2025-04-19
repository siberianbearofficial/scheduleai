from pydantic import BaseModel, Field
from openai_api.schema import *


class ResponseFormat(BaseModel):
    function_calling_needed: bool = Field(..., description="Требуется вызов одной или нескольких предоствленных функций или нет.")
    clarification_needed: bool  = Field(..., description="Нужны уточнения у пользователя. Например, запрос некорректный, то есть не хватает информации или же информация неверна." \
    "Не может быть True одновременно с function_calling_needed, то есть если нужны уточнения у пользователя вызов функций запрещен.")
    message: str = Field(..., description="Твой текстовый ответ")



