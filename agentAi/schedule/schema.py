from pydantic import BaseModel, Field
from datetime import datetime

from openai_api.schema import *
from utils.logger import logger

class GetScheduleArgs(BaseModel):
    group: str = Field(..., description="имя студента")
    date: datetime = Field(..., description="дата, на какой день нужно расписание")


test = MessagesModel.model_validate_json()

