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

INIT_CONTEXT = MessagesModel(
    root=[AGENT_ROLE_CONTEXT, RESP_FORMAT_CONTEXT]
)


