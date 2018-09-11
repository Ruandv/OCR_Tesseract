import { Component, OnInit } from "@angular/core";
import { DeviceConfigurationService } from "../services/deviceConfiguration.service";

@Component({
  selector: "app-xml-parser",
  templateUrl: "./xml-parser.component.html",
  styleUrls: ["./xml-parser.component.css"]
})
export class XmlParserComponent implements OnInit {
  constructor(private deviceConfigurationService: DeviceConfigurationService) {}
  conf: string;
  obj;

  ngOnInit() {
    this.deviceConfigurationService.getConfiguration(2).subscribe(res => {
      this.obj = res;
    });
  }
}
