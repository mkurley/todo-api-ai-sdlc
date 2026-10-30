# spec.md

## Endpoints

### GET /todos
- Returns HTTP 200
- Body is a JSON array
- For an empty store, body is `[]`

## Acceptance tests
1. GET /todos returns 200
2. GET /todos returns a JSON array
3. With no todos, the array is empty


### POST /todos
- Accepts JSON: `{ "title": "string" }`
- `title` required and non-empty; otherwise 400
- On success: HTTP 201, body is the created todo: `{ "id": <number>, "title": "<string>", "isComplete": false }`
- The new item appears in later `GET /todos` responses

## Acceptance tests (add)
4. POST /todos with a valid title returns 201 and the created todo
5. POST /todos with an empty title returns 400
6. After a successful POST, GET /todos includes that item