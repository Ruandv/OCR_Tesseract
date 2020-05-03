import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/internal/Observable";
import * as pako from 'pako';
import { map } from "rxjs/operators";
import { Region } from "src/Models/Region";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: "root"
})
export class DocumentService extends BaseService {
  controllerName = "OCR";

  constructor(private http: Http) {
    super();
  }

  getDocuments(): Observable<any> {
    return this.http
      .get(this.webApiUrl + `/${this.controllerName}/GetDocuments`)
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
    return this.http.post(this.webApiUrl + `/${this.controllerName}/UploadDocument`, data, options)
  }

  ocrDocument(fileName: string, regions: Region[]): Observable<any> {
    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.set("content-type", "application/json");
    return this.http.post(this.webApiUrl + `/${this.controllerName}/ocrDocument/${fileName}`, regions, options);
  }
}
