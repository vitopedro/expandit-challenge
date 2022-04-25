import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { last, lastValueFrom } from "rxjs";
import { environment } from 'src/environments/environment';

@Injectable()
export abstract class ChallengeApi {

    private host:string = '';

    constructor (protected httpClient: HttpClient) {
        this.host = environment.apiUrl;
    }

    public get<T>(endpoint:string, params?:{[param:string]:string}):Promise<T>{
        const queryString = this.getQueryString(params);

        let request = this.httpClient.get<T>(`${this.host}${endpoint}${queryString}`);

        return lastValueFrom(request).then(res => {
            return res;
        });
    }

    public post<T>(endpoint:string, params?:any):Promise<T>{
        let request = this.httpClient.post<T>(`${this.host}${endpoint}`, params);

        return lastValueFrom(request).then(res => {
            return res;
        });
    }

    public put<T>(endpoint:string, params?:any):Promise<T>{
        let request = this.httpClient.put<T>(`${this.host}${endpoint}`, params);

        return lastValueFrom(request).then(res => {
            return res;
        });
    }

    public delete<T>(endpoint:string):Promise<T>{
        let request = this.httpClient.delete<T>(`${this.host}${endpoint}`);

        return lastValueFrom(request).then(res => {
            return res;
        })
    }

    private getQueryString(params?:{[param:string]:string}):string{
        if(!params){
            return '';
        }
        let queryString = '?';

        for(let param in params){
            if(params[param] === undefined){
                continue;
            }

          if(queryString != '?'){
                queryString += '&';
          }

          queryString += `${param}=${params[param]}`;
        }

        return queryString != '?' ? queryString : '';
    }
}