UPDATE Toys
SET ImageUrl = CONCAT('/images/', ImageUrl)
WHERE ImageUrl NOT LIKE '/images/%';