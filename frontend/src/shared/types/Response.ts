export interface BaseResponse<T = any> {
  code: string;
  data?: T;
  error?: {
    message: string;
    details?: any;
  };
}
export interface ErrorResponse {
  message: string;
  details?: string;
}