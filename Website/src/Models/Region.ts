export class Region {
  index:number;
  regionType:string;
  description: string;
  topLeft: Point;
  width:number;
  height:number;
  // bottomRight: Point;
}

export class Point {
  x: number;
  y: number;
}


export class ModalInfo {
  header: string;
  textBinding: string;
  index: number;
}

export class ModalEmailInfo {
  header: string;
  to: string;
  subject: string;
  message: string;
}
