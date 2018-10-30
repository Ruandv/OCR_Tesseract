import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/internal/Observable";
import { pipe } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class DocumentService {
  private webApiUrl = "http://localhost:53017/api/Ocr/"; // URL to web api

  constructor(private http: Http) {}
  getDocuments(): Observable<any> {
    return this.http
      .get(this.webApiUrl + "GetDocuments")
      .pipe(map(res => res.json()));
  }

  uploadDocument(documents: Array<any>) {
    const data = new FormData();
    for (const document of documents) {
      data.append("document-name", document.file, document.file.name);
      data.append("document-type", document.type);
    }

    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.set("enctype", "multipart/form-data");

    return this.http.post(this.webApiUrl + "UploadDocument", data, options);
  }
}
