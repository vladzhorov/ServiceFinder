import axios, { AxiosError } from 'axios';
import { BaseResponse } from '../shared/types/Response';
import { AssistanceCategory } from '../shared/types/Models';
import { API_BASE_URL } from './config.ts';

export const getAssistanceCategoryById = async (categoryId: string): Promise<BaseResponse<AssistanceCategory>> => {
  try {
    const response = await axios.get<BaseResponse<AssistanceCategory>>(`${API_BASE_URL}/assistancesCategory/${categoryId}`);
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

export const createAssistanceCategory = async (category: AssistanceCategory): Promise<BaseResponse<AssistanceCategory>> => {
  try {
    const response = await axios.post<BaseResponse<AssistanceCategory>>(`${API_BASE_URL}/assistancesCategory`, category);
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
