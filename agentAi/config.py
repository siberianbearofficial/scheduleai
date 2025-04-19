from dotenv import load_dotenv
import os

load_dotenv(override=True, dotenv_path="./local.env")

TOKEN_VAR = "OPENAI_TOKEN"
API_URL_VAR = "GOAPI_URL"
LOG_LVL_VAR = "LOG_LVL"
LOG_FOLDER_PATH_VAR = "LOG_FOLDER_PATH"

# OPENAI_TOKEN=bbcbb4a8748cf29aefa39c6c03eb96b34df358e7a0b1b5cd2abffc8595be6908