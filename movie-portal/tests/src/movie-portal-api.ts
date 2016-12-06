import fetch from 'node-fetch'

export class MoviePortalApi {

    constructor(private url: string) {
    }

    public async listMoviePreviews(): Promise<MoviePreview[]> {
        const response = await fetch(`${this.url}/movies`);
        return await response.json();
    }

    public async getMovie(id: number): Promise<Movie> {
        const response = await fetch(`${this.url}/movies/${id}`);
        return await response.json();
    }

    public async listCategories(): Promise<Category[]> {
        const response = await fetch(`${this.url}/categories`);
        return await response.json();
    }
}

export interface MoviePreview {
    id: number,
    title: string,
    category: string,
    rating: number,
    year: number
}

export interface Movie extends MoviePreview {
    description: string
}

export interface Category {
    id: number,
    name: string
}