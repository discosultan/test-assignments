import { Entity, PrimaryColumn, Column, BaseEntity } from 'typeorm';

@Entity()
export default class Image extends BaseEntity {

    @PrimaryColumn()
    id: string;

    @Column()
    name: string;

    @Column()
    type: string;

    @Column()
    size: number;
}
