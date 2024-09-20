import axios, { AxiosError } from 'axios';
import { BaseResponse } from '../shared/types/Response';
import { UserProfile } from '../shared/types/Models';
import { API_BASE_URL } from './config.ts';

export const getUserProfileById = async (userId: string): Promise<BaseResponse<UserProfile>> => {
  try {
    const response = await axios.get<BaseResponse<UserProfile>>(`${API_BASE_URL}/user-profiles/${userId}`);
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

export const createUserProfile = async (profile: UserProfile): Promise<BaseResponse<UserProfile>> => {
  try {
    const response = await axios.post<BaseResponse<UserProfile>>(`${API_BASE_URL}/user-profiles`, profile);
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
