// import axios, { AxiosError, AxiosResponse } from 'axios';
// import { RESTMethod } from '../shared/types/MethodEnum.ts';
// import { orderClient } from '../shared/helpers/client.ts';

// interface Props {
//   uri: string;
//   method: RESTMethod;
//   item?: object;
//   id?: string;
// }

// type SuccessResponse<V> = {
//   code: 'success';
//   data: V;
// };

// type ErrorResponse<E extends AxiosError = AxiosError> = {
//   code: 'error';
//   error: E;
// };

// type BaseResponse<V, E extends AxiosError = AxiosError> = SuccessResponse<V> | ErrorResponse<E>;

// export const HttpRequest = async <V, E extends AxiosError = AxiosError>({
//     uri,
//     method,
//     item = {},
//     id = '',
//   }: Props): Promise<BaseResponse<V, E>> => {
//     let res: AxiosResponse<V>;
//     try {
//       console.log('Request URI:', uri);
//       console.log('Request Method:', method);
//       console.log('Request Data:', item);
//       console.log('Request ID:', id);
  
//       switch (method) {
//         case RESTMethod.Get:
//           res = await orderClient.get<V>(uri);
//           break;
//         case RESTMethod.Post:
//           res = await orderClient.post<V>(uri, item);
//           break;
//         case RESTMethod.Delete:
//           res = await orderClient.delete<V>(uri + '/' + id);
//           break;
//         case RESTMethod.Put:
//           res = await orderClient.put<V>(uri + '/' + id, item);
//           break;
//         default:
//           throw new Error('Bad request');
//       }
      
//       console.log('Response Data:', res.data);
//       return { code: 'success', data: res.data };
//     } catch (error) {
//       const axiosError = error as E;
//       console.error('Error:', axiosError.message);
//       console.error('Response Data:', axiosError.response?.data);
//       return {
//         code: 'error',
//         error: axiosError,
//       };
//     }
//   };
  