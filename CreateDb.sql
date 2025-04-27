--create db `localmooddb`

Use [localmooddb]

--Static table mostly - used for storing mood constants
CREATE TABLE Moods (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Mood VARCHAR(200) NOT NULL UNIQUE,
    Notes NVARCHAR(500)
);

INSERT INTO Moods(Mood, Notes)
VALUES 
('Feeling down', 'I am so sorry to hear that :('),
('Just okay', 'Hope you feel better tomorrow'),
('Pretty good', 'Thats good to hear'),
('Feeling amazing', 'Great!!!');

--To store users & thier roles
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
	UserRole VARCHAR(25) NOT NULL 
);


-- To store user moods

CREATE TABLE UserMoods (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    MoodId INT NOT NULL,
    MoodDate DATE NOT NULL,
    MoodTime TIME NOT NULL,
    Comment NVARCHAR(2500) NULL,
    
    -- Foreign key constraints
    CONSTRAINT FK_UserMoods_UserId FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserMoods_MoodId FOREIGN KEY (MoodId) REFERENCES Moods(Id),
    
    -- Indexes
    INDEX idx_date (MoodDate),
    INDEX idx_userid (UserId),
    INDEX idx_userid_date (UserId, MoodDate)
);



--------Creating dummy data for testing purposes ---------------
-- User 1
INSERT INTO UserMoods(UserId, MoodId, MoodDate, MoodTime, Comment)
VALUES 
(1, 1, '2025-04-01', '08:00:00', 'Feeling down today, not my best day'),
(1, 2, '2025-04-02', '09:30:00', 'Just okay, trying to get through the day'),
(1, 3, '2025-04-03', '10:15:00', 'Pretty good, work is going well'),
(1, 4, '2025-04-04', '14:45:00', 'Feeling amazing! Everything is going great'),
(1, 1, '2025-04-05', '07:45:00', 'Feeling down, need some time to recharge'),
(1, 2, '2025-04-06', '12:00:00', 'Just okay, could be better'),
(1, 3, '2025-04-07', '13:30:00', 'Pretty good, had a nice lunch'),
(1, 4, '2025-04-08', '16:00:00', 'Feeling amazing! Great workout today'),
(1, 2, '2025-04-09', '08:00:00', 'Just okay, got up late'),
(1, 3, '2025-04-10', '11:30:00', 'Pretty good, enjoying the weather'),
(1, 1, '2025-04-11', '13:00:00', 'Feeling down, stressful work day'),
(1, 4, '2025-04-12', '18:00:00', 'Feeling amazing! Great time with friends'),
(1, 2, '2025-04-13', '10:00:00', 'Just okay, not much to report'),
(1, 3, '2025-04-14', '11:00:00', 'Pretty good, got some things done today'),
(1, 4, '2025-04-15', '17:30:00', 'Feeling amazing! Finished a big project'),
(1, 1, '2025-04-16', '09:00:00', 'Feeling down, tired and overwhelmed'),
(1, 3, '2025-04-17', '15:00:00', 'Pretty good, relaxing a bit'),
(1, 2, '2025-04-18', '08:30:00', 'Just okay, trying to focus'),
(1, 4, '2025-04-19', '19:30:00', 'Feeling amazing! Great movie night'),

-- User 2
(2, 1, '2025-04-01', '08:30:00', 'Feeling down today, struggling'),
(2, 2, '2025-04-02', '09:00:00', 'Just okay, but trying to stay positive'),
(2, 3, '2025-04-03', '11:00:00', 'Pretty good, enjoying the day'),
(2, 4, '2025-04-04', '15:30:00', 'Feeling amazing! Everything seems to be going well'),
(2, 1, '2025-04-05', '10:00:00', 'Feeling down, could really use a pick-me-up'),
(2, 3, '2025-04-06', '13:00:00', 'Pretty good, having a productive morning'),
(2, 2, '2025-04-07', '14:30:00', 'Just okay, had a rough night'),
(2, 4, '2025-04-08', '16:00:00', 'Feeling amazing! Great workout today'),
(2, 1, '2025-04-09', '08:30:00', 'Feeling down, still working through it'),
(2, 3, '2025-04-10', '12:00:00', 'Pretty good, had a great breakfast'),
(2, 4, '2025-04-11', '18:00:00', 'Feeling amazing! A nice evening walk'),
(2, 1, '2025-04-12', '09:00:00', 'Feeling down, struggling a bit'),
(2, 2, '2025-04-13', '14:00:00', 'Just okay, trying to keep going'),
(2, 4, '2025-04-14', '10:30:00', 'Feeling amazing! Great start to the day'),
(2, 1, '2025-04-15', '13:00:00', 'Feeling down, but trying to stay positive'),
(2, 3, '2025-04-16', '15:00:00', 'Pretty good, happy with my progress today'),
(2, 4, '2025-04-17', '19:00:00', 'Feeling amazing! Finished everything on my list'),
(2, 2, '2025-04-18', '09:00:00', 'Just okay, just getting started today'),
(2, 1, '2025-04-19', '10:30:00', 'Feeling down, still not feeling great');
