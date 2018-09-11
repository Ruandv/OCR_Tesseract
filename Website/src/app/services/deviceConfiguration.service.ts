import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/internal/Observable";

@Injectable({
  providedIn: "root"
})
export class DeviceConfigurationService {
  private webApiUrl = "http://localhost:53017/api/DeviceConfiguration/";

  constructor(private http: HttpClient) {}
  getConfiguration(id: number): Observable<any> {
    return this.http.get(this.webApiUrl + id);
  }
}
