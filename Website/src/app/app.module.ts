import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { DocumentViewerComponent } from './document-viewer/document-viewer.component';

@NgModule({
  declarations: [AppComponent, DocumentViewerComponent],
  imports: [BrowserModule,HttpClientModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
