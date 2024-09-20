import axios, { AxiosError, AxiosResponse } from 'axios';
import { RESTMethod } from '../shared/types/MethodEnum.ts';
import { client } from '../shared/helpers/client.ts';

interface Props {
  uri: string;
  method: RESTMethod;
  item?: object;
  id?: string;
}

type SuccessResponse<V> = {
  code: 'success';
  data: V;
};

type ErrorResponse<E extends AxiosError = AxiosError> = {
  code: 'error';
  error: E;
};

type BaseResponse<V, E extends AxiosError = AxiosError> = SuccessResponse<V> | ErrorResponse<E>;

export const HttpRequest = async <V, E extends AxiosError = AxiosError>({
  uri,
  method,
  item = {},
  id = '',
}: Props): Promise<BaseResponse<V, E>> => {
  let res: AxiosResponse<V>;
  try {
    switch (method) {
      case RESTMethod.Get:
        res = await client.get<V>(uri);
        break;
      case RESTMethod.Post:
        res = await client.post<V>(uri, item);
        break;
      case RESTMethod.Delete:
        res = await client.delete<V>(uri + '/' + id);
        break;
      case RESTMethod.Put:
        res = await client.put<V>(uri + '/' + id, item);
        break;
      default:
        throw new Error('Bad request');
    }
    return { code: 'success', data: res.data };
  } catch (error) {
    const axiosError = error as E;
    return {
      code: 'error',
      error: axiosError,
    };
  }
};