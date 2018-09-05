import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/internal/Observable";

@Injectable({
  providedIn: "root"
})
export class DocumentService {
  private webApiUrl = "http://localhost:53017/api/Ocr/GetDocuments"; // URL to web api

  constructor(private http: HttpClient) {}
  getDocuments(): Observable<any> {
    return this.http.get(this.webApiUrl);
  }
}
