from pydantic import BaseModel, Field
import asyncio
import os

from openai_api.client import OpenAIClient
from openai_api.schema import *
from config import *
from utils.logger import logger
from bff_interaction.client import Client as DataClient


class TestModel(BaseModel):
    name: str = Field(..., description="мое имя")
    age: int = Field(..., examples="30")


async def main():
    openai_client = OpenAIClient(api_key=os.getenv(TOKEN_VAR))
    # deepseek_client = OpenAIClient(api_key=os.getenv(DEEPSEEK_TOKEN_VAR), base_url=os.getenv(DEEPSEEK_URL_VAR))
    data_client = DataClient()

    tools = data_client.get_tools()
    messages = data_client.get_context()
    messages.add(MessageModel(role=OpenAIRole.USER, content="Привет! Меня зовут Миша, мне 21 год! Я учусь в МГТУ в группе ИУ7-54Б. Хочу узнать свое расписание на завтра."))

    request_model = OpenAIRequestModel(
        model=OpenAIModel.O4_MINI,
        messages=messages,
        tools=tools
    )
    logger.log_json(request_model.model_dump())

    result = await openai_client.request(request_model=request_model)
    print("----------", result.choices[0].message.content, "-----------", sep='\n')
    print(result)


if __name__ == "__main__":
    asyncio.run(main())
