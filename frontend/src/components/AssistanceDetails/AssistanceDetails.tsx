import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getAssistanceById, createReview } from '../../api/AssistanceApi.ts';
import { createOrder, createOrderRequest } from '../../api/OrderApi.ts';
import { Assistance, CreateReviewViewModel, Review, CreateOrderViewModel, CreateOrderRequestViewModel } from '../../shared/types/Models';
import CommentForm from '../СommentForm/CommentForm.tsx';
import CreateOrderForm from '../CreateOrderForm/CreateOrderForm.tsx';
import CreateOrderRequestForm from '../CreateOrderRequestForm/CreateOrderRequestForm.tsx';
import { Container, Grid, Box, Typography, Card, CardContent, Button, Collapse, Rating } from '@mui/material';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

const AssistanceDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [assistance, setAssistance] = useState<Assistance | null>(null);
  const [reviews, setReviews] = useState<Review[]>([]);
  const [isOrderFormVisible, setIsOrderFormVisible] = useState(false);
  const [isOrderRequestFormVisible, setIsOrderRequestFormVisible] = useState(false);
  const [isCommentFormVisible, setIsCommentFormVisible] = useState(false);
  const [areReviewsVisible, setAreReviewsVisible] = useState(false);

  useEffect(() => {
    const fetchAssistanceAndReviews = async () => {
      try {
        const assistanceResponse = await getAssistanceById(id!);
        if (assistanceResponse.code === 'success' && assistanceResponse.data) {
          setAssistance(assistanceResponse.data);
          setReviews(assistanceResponse.data.reviews || []);
        } else {
          console.error('Error fetching assistance:', assistanceResponse.error?.message || 'Unknown error');
        }
      } catch (error) {
        console.error('Error fetching assistance and reviews:', error);
      }
    };

    fetchAssistanceAndReviews();
  }, [id]);

  const handleCommentSubmit = async (commentText: string, rating: number) => {
    const newReview: CreateReviewViewModel = {
      assistanceId: id!,
      userProfileId: '3ed7516a-8d6c-4cd2-9306-fbcd309b0308',
      comment: commentText,
      rating: rating,
    };

    try {
      const response = await createReview(newReview);
      if (response.code === 'success' && response.data) {
        setReviews(prevReviews => {
          if (response.data) {
            return [...prevReviews, response.data];
          }
          return prevReviews;
        });
      } else {
        console.error('Error creating review:', response.error?.message || 'Unknown error');
      }
    } catch (error) {
      console.error('Error creating review:', error);
    }
  };

  const handleOrderSubmit = async (orderDetails: { description?: string; scheduledDate: Date; durationInMinutes: number }) => {
    if (!assistance) return;
    const { description, scheduledDate, durationInMinutes } = orderDetails;
    const formattedScheduledDate = scheduledDate.toISOString();

    const newOrder: CreateOrderViewModel = {
      customerId: '21178a2c-f0e2-4215-b80a-149b24f68b65',
      serviceId: assistance.id,
      description: description || '',
      baseRatePerMinute: assistance.price / assistance.durationInMinutes,
      baseRateDurationInMinutes: assistance.durationInMinutes,
      durationInMinutes,
      scheduledDate: formattedScheduledDate,
    };

    try {
      const response = await createOrder(newOrder);
      if (response.code === 'success' && response.data) {
        alert('Заказ успешно создан!');
      } else {
        console.error('Error creating order:', response.error?.message || 'Unknown error');
      }
    } catch (error) {
      console.error('Error creating order:', error);
    }
  };

  const handleOrderRequestSubmit = async (requestDetails: { description: string }) => {
    const newRequest: CreateOrderRequestViewModel = {
      description: requestDetails.description,
    };

    try {
      const response = await createOrderRequest(newRequest);
      if (response.code === 'success' && response.data) {
        alert('Запрос успешно создан!');
      } else {
        console.error('Error creating order request:', response.error?.message || 'Unknown error');
      }
    } catch (error) {
      console.error('Error creating order request:', error);
    }
  };

  const togglePanel = (panel: 'order' | 'request' | 'comment' | 'reviews') => {
    switch (panel) {
      case 'order':
        setIsOrderFormVisible(prev => !prev);
        break;
      case 'request':
        setIsOrderRequestFormVisible(prev => !prev);
        break;
      case 'comment':
        setIsCommentFormVisible(prev => !prev);
        break;
      case 'reviews':
        setAreReviewsVisible(prev => !prev);
        break;
      default:
        break;
    }
  };

  if (!assistance) {
    return <p>Загрузка...</p>;
  }

  return (
    <Container>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Card>
            <CardContent>
              <Typography variant="h4" gutterBottom>{assistance.title}</Typography>
              <Typography variant="body1" paragraph>{assistance.description}</Typography>
              <Typography variant="body1">Цена: {assistance.price} ₽</Typography>
              <Typography variant="body1">Длительность: {assistance.durationInMinutes} минут</Typography>
              <Typography variant="body1">Местоположение: {assistance.location}</Typography>
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12}>
          <Card>
            <CardContent>
              <Button
                variant="contained"
                color="primary"
                fullWidth
                endIcon={areReviewsVisible ? <FaChevronUp /> : <FaChevronDown />}
                onClick={() => togglePanel('reviews')}
              >
                {areReviewsVisible ? 'Скрыть отзывы' : 'Показать отзывы'}
              </Button>
              <Collapse in={areReviewsVisible}>
                <Box mt={2}>
                  {reviews.length === 0 ? (
                    <Typography variant="body2">Нет отзывов</Typography>
                  ) : (
                    reviews.map(review => (
                      <Box key={review.id} mb={2} pb={2} borderBottom="1px solid #e0e0e0">
                        <Typography variant="body2">{review.comment}</Typography>
                        <Rating value={review.rating} readOnly />
                        <Typography variant="caption" color="textSecondary">
                          Оставлен: {new Date(review.createdAt).toLocaleString()} кем: 
                          {/* <Link to={`/profile/${review.userProfileId}`}>вава</Link> */}
                        </Typography>
                      </Box>
                    ))
                  )}
                </Box>
              </Collapse>
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12}>
          <Box mb={3}>
            <Card>
              <CardContent>
                <Button
                  variant="contained"
                  color="primary"
                  fullWidth
                  endIcon={isCommentFormVisible ? <FaChevronUp /> : <FaChevronDown />}
                  onClick={() => togglePanel('comment')}
                >
                  Добавить комментарий
                </Button>
                <Collapse in={isCommentFormVisible}>
                  <Box mt={2}>
                    <CommentForm onSubmit={handleCommentSubmit} />
                  </Box>
                </Collapse>
              </CardContent>
            </Card>
          </Box>

          <Box mb={3}>
            <Card>
              <CardContent>
                <Button
                  variant="contained"
                  color="primary"
                  fullWidth
                  endIcon={isOrderFormVisible ? <FaChevronUp /> : <FaChevronDown />}
                  onClick={() => togglePanel('order')}
                >
                  Создать заказ
                </Button>
                <Collapse in={isOrderFormVisible}>
                  <Box mt={2}>
                    <CreateOrderForm onSubmit={handleOrderSubmit} />
                  </Box>
                </Collapse>
              </CardContent>
            </Card>
          </Box>

          <Card>
            <CardContent>
              <Button
                variant="contained"
                color="primary"
                fullWidth
                endIcon={isOrderRequestFormVisible ? <FaChevronUp /> : <FaChevronDown />}
                onClick={() => togglePanel('request')}
              >
                Создать запрос
              </Button>
              <Collapse in={isOrderRequestFormVisible}>
                <Box mt={2}>
                  <CreateOrderRequestForm onSubmit={handleOrderRequestSubmit} />
                </Box>
              </Collapse>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Container>
  );
};

export default AssistanceDetail;
