import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { DocumentViewerComponent } from './document-viewer/document-viewer.component';
import { XmlParserComponent } from './xml-parser/xml-parser.component';

@NgModule({
  declarations: [AppComponent, DocumentViewerComponent, XmlParserComponent],
  imports: [BrowserModule,HttpClientModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
