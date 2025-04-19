from pydantic import BaseModel, Field
import asyncio
import os

from openai_api.api import OpenAIClient
from openai_api.schema import *
from config import *
from utils.logger import logger

class TestModel(BaseModel):
    name: str = Field(..., description="мое имя")
    age: int = Field(..., examples="30")


async def main():
    client = OpenAIClient(api_key=os.getenv(TOKEN_VAR), base_url=None)
    import schedule.schema

    # req_model = OpenAIRequestModel(model=OpenAIModel.GPT4, messages=msg_list, tools=[tool])

    # logger.log_json(req_model.model_dump())

    # result = await client.request(request_model=req_model) 
    # print(result)


if __name__ == "__main__":
    asyncio.run(main())
