import pygame
import random

# Initialize Pygame
pygame.init()

# Screen dimensions
SCREEN_WIDTH = 300
SCREEN_HEIGHT = 600
BLOCK_SIZE = 30

# Colors
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
RED = (255, 0, 0)
GREEN = (0, 255, 0)
BLUE = (0, 0, 255)
CYAN = (0, 255, 255)
MAGENTA = (255, 0, 255)
YELLOW = (255, 255, 0)
ORANGE = (255, 165, 0)

# Shapes
SHAPES = [
    [[1, 1, 1],
     [0, 1, 0]],

    [[0, 1, 1],
     [1, 1, 0]],

    [[1, 1, 0],
     [0, 1, 1]],

    [[1, 1, 1, 1]],

    [[1, 1],
     [1, 1]],

    [[0, 1, 0],
     [1, 1, 1]],

    [[1, 1, 1],
     [1, 0, 0]]
]

SHAPE_COLORS = [CYAN, BLUE, ORANGE, YELLOW, GREEN, RED, MAGENTA]

# Create screen
screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
pygame.display.set_caption("Tetris")

clock = pygame.time.Clock()

class Tetris:
    def __init__(self):
        self.board = [[0] * (SCREEN_WIDTH // BLOCK_SIZE) for _ in range(SCREEN_HEIGHT // BLOCK_SIZE)]
        self.score = 0
        self.game_over = False
        self.current_piece = self.new_piece()
        self.next_piece = self.new_piece()
        self.fall_time = 0
        self.fall_speed = 500  # Fall speed in milliseconds

    def new_piece(self):
        shape = random.choice(SHAPES)
        color = SHAPE_COLORS[SHAPES.index(shape)]
        return {'shape': shape, 'color': color, 'x': SCREEN_WIDTH // (2 * BLOCK_SIZE) - len(shape[0]) // 2, 'y': 0}

    def draw_board(self):
        for y in range(len(self.board)):
            for x in range(len(self.board[y])):
                if self.board[y][x]:
                    pygame.draw.rect(screen, self.board[y][x], (x * BLOCK_SIZE, y * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE))
        self.draw_piece(self.current_piece)

    def draw_piece(self, piece):
        for y, row in enumerate(piece['shape']):
            for x, cell in enumerate(row):
                if cell:
                    pygame.draw.rect(screen, piece['color'], ((piece['x'] + x) * BLOCK_SIZE, (piece['y'] + y) * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE))

    def rotate_piece(self):
        shape = self.current_piece['shape']
        self.current_piece['shape'] = [[shape[y][x] for y in range(len(shape))] for x in range(len(shape[0]) - 1, -1, -1)]
        if self.check_collision():
            # Rotate back if collision
            self.current_piece['shape'] = shape

    def move_piece(self, dx, dy):
        self.current_piece['x'] += dx
        self.current_piece['y'] += dy
        if self.check_collision():
            self.current_piece['x'] -= dx
            self.current_piece['y'] -= dy
            if dy:
                self.lock_piece()

    def check_collision(self):
        shape = self.current_piece['shape']
        for y, row in enumerate(shape):
            for x, cell in enumerate(row):
                if cell:
                    if (self.current_piece['y'] + y >= len(self.board) or
                        self.current_piece['x'] + x < 0 or
                        self.current_piece['x'] + x >= len(self.board[0]) or
                        self.board[self.current_piece['y'] + y][self.current_piece['x'] + x]):
                        return True
        return False

    def lock_piece(self):
        shape = self.current_piece['shape']
        for y, row in enumerate(shape):
            for x, cell in enumerate(row):
                if cell:
                    self.board[self.current_piece['y'] + y][self.current_piece['x'] + x] = self.current_piece['color']
        self.clear_lines()
        self.current_piece = self.next_piece
        self.next_piece = self.new_piece()
        if self.check_collision():
            self.game_over = True

    def clear_lines(self):
        lines_to_clear = [y for y, row in enumerate(self.board) if all(row)]
        for y in lines_to_clear:
            del self.board[y]
            self.board.insert(0, [0] * (SCREEN_WIDTH // BLOCK_SIZE))
        self.score += len(lines_to_clear)

    def update(self):
        if not self.game_over:
            self.fall_time += clock.get_rawtime()
            clock.tick()
            if self.fall_time > self.fall_speed:
                self.move_piece(0, 1)
                self.fall_time = 0

    def draw(self):
        screen.fill(BLACK)
        self.draw_board()
        pygame.display.update()

# Main game loop
tetris = Tetris()
running = True

while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_LEFT:
                tetris.move_piece(-1, 0)
            elif event.key == pygame.K_RIGHT:
                tetris.move_piece(1, 0)
            elif event.key == pygame.K_DOWN:
                tetris.move_piece(0, 1)
            elif event.key == pygame.K_UP:
                tetris.rotate_piece()

    tetris.update()
    tetris.draw()

pygame.quit()
