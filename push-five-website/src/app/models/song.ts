import { Guid } from 'guid-typescript';
import { Artist } from './artist';
import { Genre } from './genre';

export class Song {
  public id: Guid;
  public name: string;
  public artist: Artist;
  public genre: Genre;
}
