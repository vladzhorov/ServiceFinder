import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import AdBlock from '../../components/AdBlock/AdBlock.tsx';
import { UserProfile } from '../../shared/types/Models';
import {
  Container,
  Grid,
  Paper,
  Typography,
  CircularProgress,
  Avatar,
  Card,
  CardContent,
  List,
  ListItem,
  ListItemText,
  Divider,
  Box,
  Alert,
  Accordion,
  AccordionSummary,
  AccordionDetails,
  createTheme,
  ThemeProvider,
  Rating,
  Button,
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const API_URL = 'https://localhost:7091/api/usersProfile';

const theme = createTheme({
  palette: {
    primary: {
      main: '#607d8b',
    },
    secondary: {
      main: '#ffab91',
    },
    background: {
      default: '#f4f6f8', 
      paper: '#ffffff', 
    },
    text: {
      primary: '#37474f', 
      secondary: '#607d8b', 
    },
  },
  typography: {
    fontFamily: '"Roboto", "Arial", sans-serif',
  },
});

const Profile = () => {
  const { user, isAuthenticated, isLoading } = useAuth0();
  const [userProfile, setUserProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchUserProfile = async () => {
      try {
        if (isAuthenticated && user?.sub) {
          const response = await axios.get(`${API_URL}/${user.sub}`);
          setUserProfile(response.data);
        }
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchUserProfile();
  }, [isAuthenticated, user]);

  if (isLoading || loading) return <CircularProgress color="primary" />;
  if (error) return <Alert severity="error">Ошибка: {error}</Alert>;
  if (!userProfile) return <Alert severity="warning">Профиль не найден</Alert>;

  return (
    <ThemeProvider theme={theme}>
      <Container maxWidth="md">
        <Paper elevation={3} sx={{ padding: 4, marginTop: 4 }}>
          <Grid container spacing={4}>
            <Grid item xs={12} sm={4}>
              <Avatar
                src={userProfile.photoURL || ''}
                alt={userProfile.name || ''}
                sx={{ width: 120, height: 120, margin: '0 auto' }}
              />
            </Grid>
            <Grid item xs={12} sm={8}>
              <Typography variant="h4" gutterBottom color="primary">
                {userProfile.name}
              </Typography>
              <Typography variant="body1" color="textSecondary">
                Email: {userProfile.email || 'Не указан'}
              </Typography>
              <Typography variant="body1" color="textSecondary">
                Телефон: {userProfile.phoneNumber || 'Не указан'}
              </Typography>
              <Typography variant="body1" color="textSecondary" sx={{ display: 'flex', alignItems: 'center' }}>
                Рейтинг: {userProfile.rating || 'Нет рейтинга'}
                <Rating
                  value={userProfile.rating || 0}
                  precision={0.5}
                  readOnly
                  size="small"
                  sx={{ ml: 1 }}
                />
              </Typography>
              <Typography variant="body1" color="textSecondary">
                Создан: {new Date(userProfile.createdAt).toLocaleDateString()}
              </Typography>
              <Typography variant="body1" color="textSecondary">
                Обновлен: {new Date(userProfile.updatedAt).toLocaleDateString()}
              </Typography>
            </Grid>
          </Grid>

          <Box mt={4}>
            <Accordion>
              <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="services-content" id="services-header">
                <Typography variant="h5" color="primary">Предоставляемые услуги</Typography>
              </AccordionSummary>
              <AccordionDetails>
                <Grid container spacing={2}>
                  {userProfile.assistances && userProfile.assistances.length > 0 ? (
                    userProfile.assistances.map((assistance, index) => (
                      <Grid item xs={12} sm={6} key={index}>
                        <Card sx={{ backgroundColor: theme.palette.background.default }}>
                          <CardContent>
                            <Typography variant="h6" color="secondary">
                              {assistance.title}
                            </Typography>
                            <Typography variant="body2" color="textSecondary">
                              {assistance.description}
                            </Typography>
                            <Typography variant="body2" color="textSecondary">
                              Цена: {assistance.price} ₽
                            </Typography>
                            <Typography variant="body2" color="textSecondary">
                              Длительность: {assistance.durationInMinutes} минут
                            </Typography>
                            <Button 
                              component={Link} 
                              to={`/assistances/${assistance.id}`} 
                              variant="contained" 
                              color="primary"
                              sx={{ mt: 2 }}
                            >
                              Подробнее
                            </Button>
                          </CardContent>
                        </Card>
                      </Grid>
                    ))
                  ) : (
                    <Typography variant="body2" color="textSecondary">
                      Услуги не предоставлены
                    </Typography>
                  )}
                </Grid>
              </AccordionDetails>
            </Accordion>
          </Box>

          <Box mt={4}>
            <Accordion>
              <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="comments-content" id="comments-header">
                <Typography variant="h5" color="primary">Комментарии</Typography>
              </AccordionSummary>
              <AccordionDetails>
                <List>
                  {userProfile.reviews && userProfile.reviews.length > 0 ? (
                    userProfile.reviews.map((review, index) => (
                      <React.Fragment key={index}>
                        <ListItem alignItems="flex-start">
                          <ListItemText
                            primary={
                              <Typography variant="body1" color="textPrimary">
                                {/* {review.authorName || 'Аноним'} */}
                              </Typography>
                            }
                            secondary={
                              <>
                                <Typography variant="body2" color="textSecondary">
                                  {review.comment}
                                </Typography>
                                <Box mt={1}>
                                  <Rating
                                    value={review.rating}
                                    precision={0.5}
                                    readOnly
                                    size="small"
                                  />
                                </Box>
                                <Typography variant="caption" color="textSecondary">
                                  Оставлен: {new Date(review.createdAt).toLocaleString()}
                                </Typography>
                              </>
                            }
                          />
                        </ListItem>
                        <Divider />
                      </React.Fragment>
                    ))
                  ) : (
                    <Typography variant="body2" color="textSecondary">
                      Комментариев нет
                    </Typography>
                  )}
                </List>
              </AccordionDetails>
            </Accordion>
          </Box>
        </Paper>
        <Box mt={4}>
          <AdBlock />
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default Profile;
