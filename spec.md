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
