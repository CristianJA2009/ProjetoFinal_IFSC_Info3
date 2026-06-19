-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`usuario` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NOT NULL,
  `email` VARCHAR(100) NOT NULL,
  `senha` VARCHAR(25) NOT NULL,
  `foto` VARCHAR(100) NULL,
  `pontos` INT NOT NULL,
  `criado_em` DATETIME NOT NULL,
  `tipo` VARCHAR(45) NOT NULL DEFAULT 'user',
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`jogo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`jogo` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NOT NULL,
  `descricao` VARCHAR(255) NOT NULL,
  `valor` FLOAT NOT NULL,
  `capa` VARCHAR(100) NOT NULL,
  `banner` VARCHAR(100) NOT NULL,
  `criado_em` DATETIME NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`usuario_jogo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`usuario_jogo` (
  `usuario_id` INT NOT NULL,
  `jogo_id` INT NOT NULL,
  `chave_ativacao` VARCHAR(255) NOT NULL,
  `adquirido_em` DATETIME NOT NULL,
  INDEX `fk_usuario_has_jogo_jogo1_idx` (`jogo_id` ASC) VISIBLE,
  INDEX `fk_usuario_has_jogo_usuario_idx` (`usuario_id` ASC) VISIBLE,
  PRIMARY KEY (`usuario_id`, `jogo_id`),
  CONSTRAINT `fk_usuario_has_jogo_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `mydb`.`usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_usuario_has_jogo_jogo1`
    FOREIGN KEY (`jogo_id`)
    REFERENCES `mydb`.`jogo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`categoria`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`categoria` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NOT NULL,
  `jogo_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_categoria_jogo1_idx` (`jogo_id` ASC) VISIBLE,
  CONSTRAINT `fk_categoria_jogo1`
    FOREIGN KEY (`jogo_id`)
    REFERENCES `mydb`.`jogo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`compra`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`compra` (
  `idcompra` INT NOT NULL AUTO_INCREMENT,
  `valor_total` FLOAT NOT NULL,
  `criado_em` DATETIME NOT NULL,
  PRIMARY KEY (`idcompra`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`compra_jogo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`compra_jogo` (
  `compra_idcompra` INT NOT NULL,
  `jogo_id` INT NOT NULL,
  INDEX `fk_compra_has_jogo_jogo1_idx` (`jogo_id` ASC) VISIBLE,
  INDEX `fk_compra_has_jogo_compra1_idx` (`compra_idcompra` ASC) VISIBLE,
  PRIMARY KEY (`jogo_id`, `compra_idcompra`),
  CONSTRAINT `fk_compra_has_jogo_compra1`
    FOREIGN KEY (`compra_idcompra`)
    REFERENCES `mydb`.`compra` (`idcompra`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_compra_has_jogo_jogo1`
    FOREIGN KEY (`jogo_id`)
    REFERENCES `mydb`.`jogo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`carrinho`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`carrinho` (
  `idcarrinho` INT NOT NULL AUTO_INCREMENT,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`idcarrinho`),
  INDEX `fk_carrinho_usuario1_idx` (`usuario_id` ASC) VISIBLE,
  UNIQUE INDEX `usuario_id_UNIQUE` (`usuario_id` ASC) VISIBLE,
  CONSTRAINT `fk_carrinho_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `mydb`.`usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`carrinho_jogo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`carrinho_jogo` (
  `carrinho_idcarrinho` INT NOT NULL,
  `jogo_id` INT NOT NULL,
  `qtd` INT NOT NULL,
  PRIMARY KEY (`carrinho_idcarrinho`, `jogo_id`),
  INDEX `fk_carrinho_has_jogo_jogo1_idx` (`jogo_id` ASC) VISIBLE,
  INDEX `fk_carrinho_has_jogo_carrinho1_idx` (`carrinho_idcarrinho` ASC) VISIBLE,
  CONSTRAINT `fk_carrinho_has_jogo_carrinho1`
    FOREIGN KEY (`carrinho_idcarrinho`)
    REFERENCES `mydb`.`carrinho` (`idcarrinho`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_carrinho_has_jogo_jogo1`
    FOREIGN KEY (`jogo_id`)
    REFERENCES `mydb`.`jogo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`pagamento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`pagamento` (
  `idpagamento` INT NOT NULL AUTO_INCREMENT,
  `metodo` VARCHAR(100) NOT NULL,
  `pago_em` DATETIME NOT NULL,
  `compra_idcompra` INT NOT NULL,
  PRIMARY KEY (`idpagamento`),
  INDEX `fk_pagamento_compra1_idx` (`compra_idcompra` ASC) VISIBLE,
  CONSTRAINT `fk_pagamento_compra1`
    FOREIGN KEY (`compra_idcompra`)
    REFERENCES `mydb`.`compra` (`idcompra`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
