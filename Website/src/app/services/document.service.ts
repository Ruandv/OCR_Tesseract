import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/internal/Observable";
import * as pako from 'pako';
import { map } from "rxjs/operators";
import { Region } from "src/Models/Region";

@Injectable({
  providedIn: "root"
})
export class DocumentService {
  private webApiUrl = "https://localhost:44304/api/Ocr/"; // URL to web api
  // private webApiUrl = "https://localhost:5001/api/Ocr/"; // URL to web api

  constructor(private http: Http) { }
  getDocuments(): Observable<any> {
    return this.http
      .get(this.webApiUrl + "GetDocuments")
      .pipe(map(res => res.json()));
  }

  uploadDocument(documents: Array<any>) {

    const data = new FormData();
    for (const document of documents) {
      data.append("File", document.file, document.file.name);
    }

    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.set("enctype", "multipart/form-data");
    // options.headers.set("content-type","application/x-www-form-urlencoded");
    options.headers.set("Accept-Encoding", "GZIP");
    //options.headers.set("Access-Control-Allow-Headers","*")
    return this.http.post(this.webApiUrl + "UploadDocument", data, options)
  }

  ocrDocument(fileName:string,regions: Region[]): Observable<any> {
    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.set("content-type", "application/json");
    return this.http.post(this.webApiUrl + `ocrDocument/${fileName}`, regions, options);
  }
}
