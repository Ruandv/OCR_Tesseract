import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { Region } from "src/Models/Region";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class OcrTemplateService extends BaseService {

  controllerName = 'OcrTemplate';

  constructor(private http: HttpClient) {
    super();
  }

  getTemplates() {
    return this.http.get(`${this.webApiUrl}/${this.controllerName}/getTemplates`);
  }

  getTemplate(templateName): Observable<any> {
    return this.http.get(`${this.webApiUrl}/${this.controllerName}/${templateName}`);
  }

  saveTemplate(regions: Region[], fileName: string) {
    return this.http.post(this.webApiUrl + `/${this.controllerName}/${fileName}/Save`, regions);
  }

  deleteTemplate(fileName: string) {
    return this.http.delete(this.webApiUrl + `/${this.controllerName}/${fileName}`);
  }


  uploadDocument(templateName: string, documents: Array<any>) {

    const data = new FormData();
    for (const document of documents) {
      data.append("File", document.file, document.file.name);
    }

    const httpOptions = {
      headers: new HttpHeaders({
        "enctype": "multipart/form-data",
        "Accept-Encoding": "GZIP"
      })
    };

    return this.http.post(this.webApiUrl + `/${this.controllerName}/${templateName}/UploadDocument`, data, httpOptions);
  }
}
