export abstract class BaseService {
  webApiUrl = "https://localhost:5001/api"; // URL to web api
  abstract controllerName: string;
}
