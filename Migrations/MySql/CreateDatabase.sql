-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Sep 27, 2017 at 08:26 PM
-- Server version: 5.7.19-0ubuntu0.16.04.1
-- PHP Version: 7.0.22-0ubuntu0.16.04.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `scan`
--
CREATE DATABASE IF NOT EXISTS `scan` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `scan`;

-- --------------------------------------------------------

--
-- Table structure for table `Correspondents`
--

CREATE TABLE `Correspondents` (
  `Id` varchar(36) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `documents`
--

CREATE TABLE `documents` (
  `Id` varchar(36) NOT NULL,
  `PersonId` int(11) NOT NULL,
  `CorrespondentId` varchar(36) NOT NULL,
  `DocumentTypeId` varchar(36) NOT NULL,
  `Datum` date NOT NULL,
  `Description` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `DocumentTypes`
--

CREATE TABLE `DocumentTypes` (
  `Id` varchar(36) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `People`
--

CREATE TABLE `People` (
  `Id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `Scans`
--

CREATE TABLE `Scans` (
  `Id` varchar(36) NOT NULL,
  `DocumentId` varchar(36) NOT NULL,
  `Filename` text NOT NULL,
  `Datum` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Correspondents`
--
ALTER TABLE `Correspondents`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `documents`
--
ALTER TABLE `documents`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `DocumentTypes`
--
ALTER TABLE `DocumentTypes`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `People`
--
ALTER TABLE `People`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Scans`
--
ALTER TABLE `Scans`
  ADD PRIMARY KEY (`Id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
