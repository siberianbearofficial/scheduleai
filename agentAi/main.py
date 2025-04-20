from fastapi import FastAPI

from agent.router import router as agent_router

app = FastAPI(title="AIAgent")

app.include_router(agent_router, prefix="/api/agent", tags=["agent"])
