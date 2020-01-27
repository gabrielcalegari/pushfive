import { Guid } from 'guid-typescript';

export class Song {
  public id: Guid;
  public name: string;
  public artist: string;
  public genre: string;
}
