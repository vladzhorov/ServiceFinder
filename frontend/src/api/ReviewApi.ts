import axios, { AxiosError } from 'axios';
import { BaseResponse } from '../shared/types/Response.ts';
import { Review } from '../shared/types/Models.ts';
import { API_BASE_URL } from './config.ts';

export const getReviewsByAssistanceId = async (assistanceId: string): Promise<BaseResponse<Review[]>> => {
  try {
    const response = await axios.get<BaseResponse<Review[]>>(`${API_BASE_URL}/reviews/assistance/${assistanceId}`);
    return response.data;
  } catch (error) {
    const axiosError = error as AxiosError;
    return {
      code: 'error',
      error: {
        message: axiosError.message,
        details: axiosError.response?.data,
      },
    };
  }
};

export const createReview = async (review: Review): Promise<BaseResponse<Review>> => {
  try {
    const response = await axios.post<BaseResponse<Review>>(`${API_BASE_URL}/reviews`, review);
    return response.data;
  } catch (error) {
    const axiosError = error as AxiosError;
    return {
      code: 'error',
      error: {
        message: axiosError.message,
        details: axiosError.response?.data,
      },
    };
  }
};
