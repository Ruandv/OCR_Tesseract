import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions } from "@angular/http";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { Region } from "src/Models/Region";

@Injectable({
  providedIn: "root"
})
export class OcrTemplateService extends BaseService {

  controllerName = 'OcrTemplate';

  constructor(private http: Http) {
    super();
  }

  getTemplates() {
    return this.http.get(`${this.webApiUrl}/${this.controllerName}/getTemplates`);
  }

  getTemplate(templateName): Observable<any> {
    return this.http.get(`${this.webApiUrl}/${this.controllerName}/getTemplate/${templateName}`);
  }

  saveTemplate(regions: Region[], fileName: string) {
    debugger;
    return this.http.post(this.webApiUrl + `/${this.controllerName}/${fileName}/Save`, regions);
  }

  uploadDocument(templateName: string, documents: Array<any>) {

    const data = new FormData();
    for (const document of documents) {
      data.append("File", document.file, document.file.name);
    }

    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.set("enctype", "multipart/form-data");
    options.headers.set("Accept-Encoding", "GZIP");
    return this.http.post(this.webApiUrl + `/${this.controllerName}/UploadDocument/${templateName}`, data, options);
  }
}
