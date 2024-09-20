import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  TextField,
  Button,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
  Container,
  Typography,
  Box,
  Paper,
} from '@mui/material';
import { createAssistance, getAssistanceCategories } from '../../api/AssistanceApi.ts';
import { CreateAssistanceViewModel, AssistanceCategory } from '../../shared/types/Models.ts';
import { useAssistanceStore } from '../../store/useAssistanceStore.ts';

const CreateAssistance = () => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState(0);
  const [durationInMinutes, setDurationInMinutes] = useState(0);
  const [location, setLocation] = useState('');
  const [categories, setCategories] = useState<AssistanceCategory[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<string>('');
  const navigate = useNavigate();
  const addAssistance = useAssistanceStore((state) => state.addAssistance);
  const fetchAssistances = useAssistanceStore((state) => state.fetchAssistances);

  const userProfileId = '21178a2c-f0e2-4215-b80a-149b24f68b65';

  useEffect(() => {
    const fetchCategories = async () => {
      const response = await getAssistanceCategories(1, 5);
      if (response.code === 'success' && response.data) {
        setCategories(response.data.data);
      } else {
        console.error('Error fetching categories:', response.error?.message || 'Unknown error');
      }
    };

    fetchCategories();
  }, []);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const assistance: CreateAssistanceViewModel = {
      userProfileId,
      assistanceCategoryId: selectedCategory,
      title,
      description,
      price,
      durationInMinutes,
      location,
    };

    const response = await createAssistance(assistance);
    if (response.code === 'success' && response.data) {
      addAssistance(response.data);
      await fetchAssistances(1, 10);
      navigate('/');
    } else {
      console.error('Error creating assistance:', response.error?.message || 'Unknown error');
    }
  };

  return (
    <Container maxWidth="sm">
      <Paper elevation={3} sx={{ padding: 4, mt: 4 }}>
        <Typography variant="h4" gutterBottom>
          Создать Объявление
        </Typography>
        <Box component="form" onSubmit={handleSubmit} sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="Название"
            variant="outlined"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />
          <TextField
            label="Описание"
            variant="outlined"
            multiline
            rows={4}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
          <TextField
            label="Цена"
            variant="outlined"
            type="number"
            value={price}
            onChange={(e) => setPrice(Number(e.target.value))}
            required
          />
          <TextField
            label="Длительность (минуты)"
            variant="outlined"
            type="number"
            value={durationInMinutes}
            onChange={(e) => setDurationInMinutes(Number(e.target.value))}
            required
          />
          <TextField
            label="Местоположение"
            variant="outlined"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
            required
          />
          <FormControl variant="outlined" required>
            <InputLabel>Категория</InputLabel>
            <Select
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
              label="Категория"
            >
              <MenuItem value="">
                <em>Выберите категорию</em>
              </MenuItem>
              {categories.map((category) => (
                <MenuItem key={category.id} value={category.id}>
                  {category.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <Button type="submit" variant="contained" color="primary">
            Создать Объявление
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export default CreateAssistance;
