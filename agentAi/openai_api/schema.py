from pydantic import BaseModel, RootModel, Field
from enum import Enum
from typing import Annotated, Optional, Any, Type, TypedDict
from datetime import datetime


class OpenAIModel(str, Enum):
    TURBO = "gpt-3.5-turbo"
    GPT4 = "gpt-4.1"
    GPT4_TURBO = "gpt-4-turbo-preview"
    O4_MINI = "gpt-4o-2024-11-20"
    DEEPSEEK = "deepseek-chat"


class OpenAIRole(str, Enum):
    SYSTEM = "system"
    USER = "user"
    ASSIST = "assistant"
    TOOL = "tool"

class ParametersDesc(TypedDict):
    type: str
    format: str
    description: str


class ParametersModel(BaseModel):
    type: Annotated[str, Field(description="Тип схемы — всегда 'object' для объекта параметров функции.")] = "object"
    properties: dict[str, ParametersDesc] = Field(..., examples= {
            "date": {
                "type": "string",
                "format": "date-time",
                "description": "Дата, на которую нужно получить расписание. Формат ISO 8601, например 2025-04-20T00:00:00"
            }
        },
        description="Список параметров, которые принимает функция."
    )
    required: list[str] = Field(..., examples=["group", "date"], description="Обязательные параметры.")



class ToolFunction(BaseModel):
    """Описание вызываемой функции"""
    name: str = Field(..., description="Название функции")
    description: str = Field(..., description="Описание что делает функция")
    parameters: ParametersModel = Field(..., description="Аргументы функции в формате JSON-строки")


class ToolModel(BaseModel):
    type: str = Field("function", description="Тип вызова, всегда 'function' для Function Calling")
    function: ToolFunction = Field(...)


class ToolsModel(RootModel):
    root: list[ToolModel] = Field(...)


class MessageModel(BaseModel):
    role: OpenAIRole = Field(..., examples=["user"])
    content: Optional[str] = Field(None, examples=["tell me about Lomonosov"], description="Содержимое сообщения")


class MessagesModel(RootModel):
    root: list[MessageModel] = Field(...)
    
    def _create_date_model(self) -> MessageModel:
        return MessageModel(role=OpenAIRole.SYSTEM, content=f"current date in ISO 8601 FORMAT: {datetime.now().isoformat()}")

    # обновляет (или добавляет, если отсутствует) контекст текущего времени
    def _update_date(self) -> None:
        current_date_model = self._create_date_model()
        updated = False
        for i, msg in enumerate(self.root):
            if msg.role == OpenAIRole.SYSTEM and msg.content.startswith("current date"):
                self.root[i].content = current_date_model
                updated = True
        
        if not updated:
            self.root.append(current_date_model)
    
    def add(self, msg: MessageModel) -> None:
        self.root.append(msg)
        

class OpenAIRequestModel(BaseModel):
    model: OpenAIModel = Field(..., examples="gpt-4")
    messages: MessagesModel = Field(...)
    user: Optional[str] = Field(None, examples=["Misha Kozin IU7-64B"], description="Имя участника диалога")
    tools: Optional[ToolsModel] = Field(None)
    tool_choice: Optional[str] = Field("auto", description="какую функцию вызывать")

    def get_messages(self) -> list[dict[str, Any]]:
        return [msg.model_dump() for msg in self.messages.root]

    def get_tools(self) -> list[dict[str, Any]] | None:
        if self.tools is None:
            return None
        return [tool.model_dump() for tool in self.tools.root]