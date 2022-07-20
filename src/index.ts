import { db } from './data-source';

try {
  console.log('Initializing database');
  await db.initialize();
  console.log('Database initialized. Initializing express.');

  

} catch (err) {
  console.error(err);
}