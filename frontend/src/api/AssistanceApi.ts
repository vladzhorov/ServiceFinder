import { HttpRequest } from './GenericApi.ts';
import { RESTMethod } from '../shared/types/MethodEnum.ts';
import { BaseResponse } from '../shared/types/Response.ts';
import { Assistance, CreateAssistanceViewModel, AssistanceCategory, PagedResult, Review, CreateReviewViewModel } from '../shared/types/Models.ts';

export const createAssistance = async (assistance: CreateAssistanceViewModel): Promise<BaseResponse<Assistance>> => {
  return HttpRequest<Assistance>({
    uri: '/assistances',
    method: RESTMethod.Post,
    item: assistance,
  });
};

export const getAssistances = async (pageNumber: number, pageSize: number): Promise<BaseResponse<PagedResult<Assistance>>> => {
  return HttpRequest<PagedResult<Assistance>>({
    uri: `/assistances?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    method: RESTMethod.Get,
  });
};

export const getAssistanceCategories = async (pageNumber: number, pageSize: number): Promise<BaseResponse<PagedResult<AssistanceCategory>>> => {
  return HttpRequest<PagedResult<AssistanceCategory>>({
    uri: `/assistancesCategory?pageNumber=${pageNumber}&pageSize=${pageSize}`, 
    method: RESTMethod.Get,
  });
};

export const getAssistanceById = async (id: string): Promise<BaseResponse<Assistance>> => {
  return HttpRequest<Assistance>({
    uri: `/assistances/${id}`,
    method: RESTMethod.Get,
  });
};

export const getReviewsByAssistanceId = async (id: string, pageNumber: number, pageSize: number): Promise<BaseResponse<PagedResult<Review>>> => {
  return HttpRequest<PagedResult<Review>>({
    uri: `/api/reviews/${id}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    method: RESTMethod.Get,
  });
};

export const createReview = async (review: CreateReviewViewModel): Promise<BaseResponse<Review>> => {
  return HttpRequest<Review>({
    uri: `reviews`,
    method: RESTMethod.Post,
    item: review,
  });
};
