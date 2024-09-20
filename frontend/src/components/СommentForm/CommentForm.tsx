import React, { useState } from 'react';
import { TextField, Button, Box, Typography, Rating, Paper } from '@mui/material';

interface CommentFormProps {
  onSubmit: (commentText: string, rating: number) => void;
}

const CommentForm: React.FC<CommentFormProps> = ({ onSubmit }) => {
  const [commentText, setCommentText] = useState('');
  const [rating, setRating] = useState<number | null>(5);

  const handleSubmit = () => {
    if (commentText.trim() && rating) {
      onSubmit(commentText, rating);
      setCommentText('');
    }
  };

  return (
    <Paper elevation={3} sx={{ padding: 4 }}>
      <Typography variant="h5" gutterBottom>Оставить комментарий</Typography>
      <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
        <TextField
          label="Ваш комментарий"
          variant="outlined"
          multiline
          rows={4}
          value={commentText}
          onChange={(e) => setCommentText(e.target.value)}
        />
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Typography component="legend">Рейтинг:</Typography>
          <Rating
            value={rating}
            onChange={(e, newValue) => setRating(newValue)}
            precision={1}
          />
        </Box>
        <Button onClick={handleSubmit} variant="contained" color="primary">Отправить</Button>
      </Box>
    </Paper>
  );
};

export default CommentForm;
