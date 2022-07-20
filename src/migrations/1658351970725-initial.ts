import { MigrationInterface, QueryRunner } from 'typeorm';

export class initial1658351970725 implements MigrationInterface {
    name = 'initial1658351970725';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`
            CREATE TABLE "user" (
                "id" integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                "firstName" varchar NOT NULL,
                "lastName" varchar NOT NULL,
                "age" integer NOT NULL
            )
        `);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`
            DROP TABLE "user"
        `);
    }

}
