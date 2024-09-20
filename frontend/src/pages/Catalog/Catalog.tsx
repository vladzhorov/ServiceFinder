import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import {
  Container,
  Grid,
  Typography,
  Card,
  CardContent,
  Button,
  Box,
  Paper,
  List,
  ListItem,
  ListItemText,
  Divider,
  createTheme,
  ThemeProvider,
} from '@mui/material';
import { AssistanceCategory } from '../../shared/types/Models.ts';
import { getAssistanceCategories } from '../../api/AssistanceApi.ts';

// Создание кастомной темы
const theme = createTheme({
  palette: {
    primary: {
      main: '#607d8b', // Мягкий серо-голубой цвет
    },
    secondary: {
      main: '#ffab91', // Мягкий коралловый цвет
    },
    background: {
      default: '#f4f6f8', // Светлый фон
      paper: '#ffffff', // Цвет бумаги для карточек
    },
    text: {
      primary: '#37474f', // Темный текст
      secondary: '#607d8b', // Текст посветлее
    },
  },
  typography: {
    fontFamily: '"Roboto", "Arial", sans-serif',
  },
});

const Catalog: React.FC = () => {
  const [categories, setCategories] = useState<AssistanceCategory[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<AssistanceCategory | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      const response = await getAssistanceCategories(1, 10);
      if (response.code === 'success' && response.data) {
        setCategories(response.data.data);
      } else {
        console.error('Error fetching categories:', response.error?.message || 'Unknown error');
      }
    };

    fetchCategories();
  }, []);

  const handleCategoryClick = (category: AssistanceCategory) => {
    setSelectedCategory(category);
  };

  const handleBackToCategories = () => {
    setSelectedCategory(null);
  };

  return (
    <ThemeProvider theme={theme}>
      <Container maxWidth="md" sx={{ mt: 4 }}>
        <Typography variant="h3" color="primary" gutterBottom>
          Каталог
        </Typography>
        {selectedCategory ? (
          <Paper elevation={3} sx={{ padding: 3 }}>
            <Button
              variant="contained"
              color="secondary"
              onClick={handleBackToCategories}
              sx={{ mb: 2 }}
            >
              Назад
            </Button>
            <Typography variant="h4" color="primary" gutterBottom>
              {selectedCategory.name}
            </Typography>
            <Typography variant="body1" color="textSecondary" paragraph>
              {selectedCategory.description}
            </Typography>
            <List>
              {selectedCategory.assistances && selectedCategory.assistances.length > 0 ? (
                selectedCategory.assistances.map((assistance) => (
                  <React.Fragment key={assistance.id}>
                    <ListItem alignItems="flex-start">
                      <ListItemText
                        primary={
                          <Typography variant="h6" color="textPrimary">
                            {assistance.title}
                          </Typography>
                        }
                        secondary={
                          <>
                            <Typography variant="body2" color="textSecondary">
                              {assistance.description}
                            </Typography>
                            <Box mt={1}>
                              <Typography variant="body2" color="textSecondary">
                                Цена: {assistance.price}
                              </Typography>
                              <Typography variant="body2" color="textSecondary">
                                Длительность: {assistance.durationInMinutes} минут
                              </Typography>
                              <Typography variant="body2" color="textSecondary">
                                Местоположение: {assistance.location}
                              </Typography>
                            </Box>
                          </>
                        }
                      />
                      <Button
                        variant="contained"
                        color="primary"
                        component={Link}
                        to={`/assistances/${assistance.id}`}
                        sx={{ mt: 2 }}
                      >
                        Подробнее
                      </Button>
                    </ListItem>
                    <Divider />
                  </React.Fragment>
                ))
              ) : (
                <Typography variant="body2" color="textSecondary">
                  Нет доступных объявлений в этой категории.
                </Typography>
              )}
            </List>
          </Paper>
        ) : (
          <Grid container spacing={4}>
            {categories.map((category) => (
              <Grid item xs={12} sm={6} key={category.id}>
                <Card
                  sx={{
                    backgroundColor: theme.palette.background.paper,
                    cursor: 'pointer',
                    '&:hover': {
                      boxShadow: 6,
                    },
                  }}
                  onClick={() => handleCategoryClick(category)}
                >
                  <CardContent>
                    <Typography variant="h5" color="primary">
                      {category.name}
                    </Typography>
                    <Typography variant="body2" color="textSecondary">
                      {category.description}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>
        )}
      </Container>
    </ThemeProvider>
  );
};

export default Catalog;
