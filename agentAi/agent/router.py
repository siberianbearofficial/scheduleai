from fastapi import APIRouter, Query, Body

from agent.schema import *
from agent.service import ai_agent

router = APIRouter()


@router.post(path="/request", response_model=AgentResponseModel)
async def agent_request(
    university: str = Query(..., 
                          description="Университет студента",
                          examples=["МГТУ им. Баумана"]),
    
    group: str = Query(...,
                      description="Группа студента",
                      examples=["ИУ7-64Б"]),
    user_message: Optional[str] = Query(None, examples=["Привет, во сколько завтра я могу подойти к Рязановой.Н.Ю?"],
                                    description="Сообщение от пользователя. None если endpoint вызывается для передачи агенту результатов выполнения функций.Не может быть None одновременно с messages!"),

    user: Optional[str] = Query(None, examples=["Misha Kozin IU7-64B"], description="Можно передать имя пользователя"),
    agent_request: Optional[AgentRequestModel] = Body(None)

    ) -> AgentResponseModel:
    
    result = await ai_agent.request(group=group, university=university, agent_request=agent_request, user_message=user_message, user=user)
    
    return result


@router.post(path="/add-function-results", response_model=AgentResponseModel)
async def add_function_results(
    tool_call_id: str = Query(...,
                              description="id вызванной функции из messages.tool_calls",
                              examples=["call_qxRt1h8a8HDwG4HEe2QQCmeT"]),
    payload: FunctionResultsModel = Body(...)
) -> AgentResponseModel:
    messages = payload.messages
    messages.add(ToolCallResult(tool_call_id=tool_call_id, content=payload.function_result))

    return AgentResponseModel(messages=messages)
