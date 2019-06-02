import { Entity, PrimaryColumn, Column, BaseEntity } from 'typeorm';

@Entity()
export default class User extends BaseEntity {

    // Not that we may want to use a separate id field. This is to support users changing their
    // user name.
    @PrimaryColumn()
    userName: string;

    @Column()
    firstName: string;

    @Column()
    lastName: string;

    @Column()
    password: string;

    @Column()
    avatar: string;
}
