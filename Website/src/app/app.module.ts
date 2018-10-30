import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { DocumentViewerComponent } from './document-viewer/document-viewer.component';
import { HttpModule } from "@angular/http";

@NgModule({
  declarations: [AppComponent, DocumentViewerComponent],
  imports: [BrowserModule,HttpModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
