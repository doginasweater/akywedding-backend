import { db } from './data-source.js';
import express from 'express';

const port = 3000;

try {
  console.log('Initializing database');
  await db.initialize();
  console.log('Database initialized. Initializing express.');

  const app = express();

  app.listen(port, () => {
    console.log(`App now listening on port ${port}`);
  });
} catch (err) {
  console.error(err);
}