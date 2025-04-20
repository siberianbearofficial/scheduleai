from pydantic import BaseModel, RootModel, Field
from enum import Enum
from typing import Annotated, Optional, Any, TypedDict, Literal
from datetime import datetime
from openai.types.chat import ChatCompletionMessageToolCall

from openai_api.schema import *


class AgentRequestModel(BaseModel):
    messages: MessagesModel = Field(...)


class AgentResponseModel(BaseModel):
    messages: MessagesModel = Field(..., description="messages от gpt с историей сообщений и его ответов")


class FunctionResultsModel(BaseModel):
    messages: MessagesModel = Field(..., )
    function_result: str = Field(..., description="Результат выполнения функции в текстовом виде (gpt сам будет его анализировать)")
