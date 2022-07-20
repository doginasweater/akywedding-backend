import 'reflect-metadata';
import { DataSource } from 'typeorm';
import { User } from './models/User.js';

// export const db = new DataSource({
//   type: 'postgres',
//   host: 'localhost',
//   port: 5432,
//   username: 'test',
//   password: 'test',
//   database: 'test',
//   synchronize: true,
//   logging: false,
//   entities: [ User ],
//   migrations: [],
//   subscribers: [],
// });

export const db = new DataSource({
  type: 'sqlite',
  database: 'testing.db',
  entities: [ User ],
});
