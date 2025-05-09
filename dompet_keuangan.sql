-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: May 09, 2025 at 03:14 AM
-- Server version: 8.4.3
-- PHP Version: 8.3.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dompet_keuangan`
--

-- --------------------------------------------------------

--
-- Table structure for table `pendapatan`
--

CREATE TABLE `pendapatan` (
  `id` int NOT NULL,
  `jumlah` decimal(12,2) NOT NULL,
  `kategori` varchar(50) NOT NULL,
  `deskripsi` varchar(255) DEFAULT NULL,
  `tanggal` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `pendapatan`
--

INSERT INTO `pendapatan` (`id`, `jumlah`, `kategori`, `deskripsi`, `tanggal`) VALUES
(1, 1000000.00, 'Gaji', 'Gaji bulan Mei', '2025-05-06 18:00:51'),
(2, 500000.00, 'Bonus', 'lembur', '2025-05-06 18:01:01');

--
-- Triggers `pendapatan`
--
DELIMITER $$
CREATE TRIGGER `after_insert_pendapatan` AFTER INSERT ON `pendapatan` FOR EACH ROW BEGIN
    UPDATE saldo 
    SET total_pendapatan = (SELECT COALESCE(SUM(jumlah),0) FROM pendapatan),
        saldo_akhir = (SELECT COALESCE(SUM(jumlah),0) FROM pendapatan) - 
                     (SELECT COALESCE(SUM(jumlah),0) FROM pengeluaran),
        tanggal_update = NOW()
    WHERE id = 1;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `pengeluaran`
--

CREATE TABLE `pengeluaran` (
  `id` int NOT NULL,
  `jumlah` decimal(12,2) NOT NULL,
  `kategori` varchar(50) NOT NULL,
  `deskripsi` varchar(255) DEFAULT NULL,
  `tanggal` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `pengeluaran`
--

INSERT INTO `pengeluaran` (`id`, `jumlah`, `kategori`, `deskripsi`, `tanggal`) VALUES
(1, 200000.00, 'Makanan', 'Makan siang', '2025-05-06 18:00:51');

--
-- Triggers `pengeluaran`
--
DELIMITER $$
CREATE TRIGGER `after_insert_pengeluaran` AFTER INSERT ON `pengeluaran` FOR EACH ROW BEGIN
    UPDATE saldo 
    SET total_pengeluaran = (SELECT COALESCE(SUM(jumlah),0) FROM pengeluaran),
        saldo_akhir = (SELECT COALESCE(SUM(jumlah),0) FROM pendapatan) - 
                     (SELECT COALESCE(SUM(jumlah),0) FROM pengeluaran),
        tanggal_update = NOW()
    WHERE id = 1;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `saldo`
--

CREATE TABLE `saldo` (
  `id` int NOT NULL DEFAULT '1',
  `total_pendapatan` decimal(12,2) NOT NULL DEFAULT '0.00',
  `total_pengeluaran` decimal(12,2) NOT NULL DEFAULT '0.00',
  `saldo_akhir` decimal(12,2) NOT NULL DEFAULT '0.00',
  `tanggal_update` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `saldo`
--

INSERT INTO `saldo` (`id`, `total_pendapatan`, `total_pengeluaran`, `saldo_akhir`, `tanggal_update`) VALUES
(1, 1500000.00, 200000.00, 1300000.00, '2025-05-06 18:01:20');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `pendapatan`
--
ALTER TABLE `pendapatan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pengeluaran`
--
ALTER TABLE `pengeluaran`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `saldo`
--
ALTER TABLE `saldo`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `pendapatan`
--
ALTER TABLE `pendapatan`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `pengeluaran`
--
ALTER TABLE `pengeluaran`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
