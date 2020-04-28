export class Region {
  description: string;
  topLeft: Point;
  bottomRight: Point;
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
