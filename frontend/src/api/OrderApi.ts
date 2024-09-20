import { HttpRequest } from './GenericApi2.ts';
import { RESTMethod } from '../shared/types/MethodEnum.ts';
import { BaseResponse } from '../shared/types/Response.ts';
import { CreateOrderViewModel,CreateOrderRequestViewModel, Order, OrderRequest } from '../shared/types/Models.ts';


export const createOrder = async (orderDetails: CreateOrderViewModel): Promise<BaseResponse<Order>> => {
  return HttpRequest<Order>({
    uri: `/orders`,
    method: RESTMethod.Post,
    item: orderDetails,
  });
};

export const createOrderRequest = async (orderRequest: CreateOrderRequestViewModel) : Promise<BaseResponse<OrderRequest>> => {
  return HttpRequest<OrderRequest>({
    uri: `/order-requests`,
    method: RESTMethod.Post,
    item: orderRequest,
  });
};