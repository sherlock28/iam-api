#!/usr/bin/env python3
"""
.NET Database Migration Tool
Generic script to apply Entity Framework migrations
"""

import os
import sys
import subprocess
import argparse
from pathlib import Path

class Colors:
    """Colors for console output"""
    GREEN = '\033[92m'
    RED = '\033[91m'
    YELLOW = '\033[93m'
    BLUE = '\033[94m'
    BOLD = '\033[1m'
    END = '\033[0m'

class MigrationRunner:
    def __init__(self, project_path: str):
        self.project_path = Path(project_path).resolve()

    def print_header(self):
        """Prints the script header"""
        print(f"{Colors.BLUE}{'=' * 50}{Colors.END}")
        print(f"{Colors.BLUE}{Colors.BOLD}  .NET Database Migration Tool{Colors.END}")
        print(f"{Colors.BLUE}{'=' * 50}{Colors.END}")
        print()

    def print_success(self, message: str):
        """Prints success message"""
        print(f"{Colors.GREEN}✅ {message}{Colors.END}")

    def print_error(self, message: str):
        """Prints error message"""
        print(f"{Colors.RED}❌ {message}{Colors.END}")

    def print_info(self, message: str):
        """Prints informational message"""
        print(f"{Colors.BLUE}ℹ️  {message}{Colors.END}")

    def check_dotnet_installed(self) -> bool:
        """Checks if .NET SDK is installed"""
        try:
            result = subprocess.run(['dotnet', '--version'],
                                  capture_output=True, text=True, check=True)
            version = result.stdout.strip()
            self.print_info(f".NET SDK version: {version}")
            return True
        except (subprocess.CalledProcessError, FileNotFoundError):
            self.print_error(".NET SDK not found. Please install .NET 8 SDK.")
            return False

    def check_project_exists(self) -> bool:
        """Checks if the migration project exists"""
        if not self.project_path.exists():
            self.print_error(f"Migration project directory not found: {self.project_path}")
            return False

        # Look for .csproj file
        csproj_files = list(self.project_path.glob("*.csproj"))
        if not csproj_files:
            self.print_error(f"No .csproj file found in: {self.project_path}")
            return False

        self.print_info(f"Project found: {csproj_files[0].name}")
        return True

    def run_migration(self) -> bool:
        """Runs the migrations"""
        try:
            # Change to project directory
            original_cwd = os.getcwd()
            os.chdir(self.project_path)

            # Run migrations
            self.print_info("Applying migrations...")

            result = subprocess.run([
                'dotnet', 'run',
                '--configuration', 'Release',
                '--environment', 'Production'
            ], text=True)

            if result.returncode == 0:
                self.print_success("Migrations applied successfully!")
                return True
            else:
                self.print_error(f"Migration failed with exit code: {result.returncode}")
                return False

        except Exception as e:
            self.print_error(f"Unexpected error: {str(e)}")
            return False
        finally:
            # Restore original directory
            os.chdir(original_cwd)

def main():
    parser = argparse.ArgumentParser(
        description=".NET Database Migration Tool"
    )

    parser.add_argument(
        '-p', '--project-path',
        type=str,
        required=True,
        help='Path to the migrations project directory'
    )

    args = parser.parse_args()

    # Create the runner
    runner = MigrationRunner(args.project_path)
    runner.print_header()

    # Validations
    if not runner.check_dotnet_installed():
        sys.exit(1)

    if not runner.check_project_exists():
        sys.exit(1)

    # Execute migrations
    success = runner.run_migration()

    if success:
        print()
        runner.print_success("Operation completed successfully!")
        sys.exit(0)
    else:
        print()
        runner.print_error("Operation failed!")
        sys.exit(1)

if __name__ == "__main__":
    main()
